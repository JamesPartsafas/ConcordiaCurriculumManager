FROM node:alpine

WORKDIR /app

COPY package.json .
COPY package-lock.json .

# This line will only run if the package.json or package-lock.json files have changed
RUN npm install

# Copy the rest of the files. This will only run if the files have changed.
# This is to prevent the npm install from running every time a file changes.
# This is a good practice to speed up the build process, it optimizes caching.
COPY . .

RUN npm run build

EXPOSE 4173


CMD [ "npm", "run", "preview" ]

# A good practice to take advantage of caching is to copy the files that change the least first.