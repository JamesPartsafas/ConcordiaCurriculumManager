#!/bin/bash

SERVICE_ID=$1
API_KEY=$2
KEY_VALUE=$3
DB_STRING=$4
SENDER_EMAIL=$5
SENDER_PASS=$6
MAX_RETRIES=3
RETRY_COUNT=0

function make_request {
    response_body_file=$(mktemp)
    response_headers_file=$(mktemp)
    local response=$(curl -o "$response_body_file" -D "$response_headers_file" --request PUT --url "https://api.render.com/v1/services/${SERVICE_ID}/env-vars" --header 'accept: application/json' --header "Authorization: Bearer ${API_KEY}" --header 'content-type: application/json' --data '[{"key": "IdentitySettings__Key","value": "'"${KEY_VALUE}"'"},{"key": "IdentitySettings__Issuer","value": "https://ccm-backend.onrender.com"},{"key": "IdentitySettings__SecurityAlgorithms","value": "HS256"},{"key": "IdentitySettings__Audience","value": "ccm-backend"},{"key": "DatabaseSettings__ConnectionString","value": "'"${DB_STRING}"'"},{"key": "CorsSettings__AllowedWebsite","value": "https://ccm-frontend.onrender.com"},{"key": "AllowedHosts","value": "ccm-backend.onrender.com"},{"key": "Serilog__WriteTo__0__Args__connectionString","value": "'"${DB_STRING}"'"},{"key": "SenderEmailSettings__SenderSMTPHost","value":"smtp.gmail.com"},{"key":"SenderEmailSettings__SenderSMTPPort","value":"587"},{"key":"SenderEmailSettings__SenderEmail","value":"'"${SENDER_EMAIL}"'"},{"key":"SenderEmailSettings__SenderPassword","value":"'"${SENDER_PASS}"'"}]')
    
    local response_headers=$(cat "$response_headers_file")
    local response_body=$(cat "$response_body_file")
    rm -f "$response_headers_file" "$response_body_file"

    local response_code=$(echo "$response_headers" | head -n 1 | awk '{print $2}')
    local ratelimit_reset=$(echo "$response_headers" | grep -i "Ratelimit-Reset" | awk '{print $2}')
    
    echo "$response_code $ratelimit_reset $response_body"
}

function censor_output {
    local input

    if [[ -n "$1" ]]; then
        input="$1"
    else
        input=$(cat)
    fi

    if [[ -z "$input" ]]; then
      echo "Input cannot be null"
      exit 1
    fi

    local result=$(echo "$input" | sed "s/\($SERVICE_ID\|$API_KEY\|$KEY_VALUE\|$DB_STRING\|$SENDER_EMAIL\|$SENDER_PASS\)/CENSORED/g")
    echo "$result"
}

function main {
    local response_info=$(make_request)
    local response_code=$(echo "$response_info" | awk '{print $1}')
    local ratelimit_reset=$(echo "$response_info" | awk '{print $2}')
    local response_body=$(echo "$response_info" | awk '{sub(/([^ ]* ){2}/, ""); print}' | censor_output)

    if [[ $response_code -eq 429 ]]; then
        RETRY_COUNT=$((RETRY_COUNT + 1))
        local current_timestamp=$(date +%s)
        local wait_date=$((current_timestamp + ratelimit_reset))

        if [[ RETRY_COUNT -ge MAX_RETRIES ]]; then
            echo "Max Attempts Reached"
            exit 1
        fi

        echo "Rate limit exceeded. Waiting until $(date -d @$wait_date) to retry (Attempt $RETRY_COUNT)..."
        while [[ $ratelimit_reset -gt 0 ]]; do
            echo "Waiting for 10 seconds..."
            sleep 10
            local ratelimit_reset=$((ratelimit_reset - 10))
        done
        
        echo "Resuming..."
        main
    elif [[ $((response_code / 100)) -eq 2 ]]; then
        echo "Applied the prod appsettings successfully"
    else
        echo "Error: Request failed with response code $response_code"
        echo "Response: $response_body"
        exit 1 
    fi
}

main