export default {
    preset: "ts-jest",
    testEnvironment: "jest-environment-jsdom",
    testMatch: ["**/***.test.tsx"],
    moduleFileExtensions: ["js", "jsx", "tsx", "ts"],
    rootDir: "__tests__",
    transform: {
        "^.+\\.tsx?$": [
            "ts-jest",
            {
                tsconfig: "./tsconfig-test.json",
                diagnostics: {
                    ignoreCodes: ["TS151001"],
                },
            },
        ],

        "^.+\\.jsx?$": "babel-jest",
    },
    transformIgnorePatterns: ["node_modules/(?!(d3-selection|another-module-you-need-to-transform)/)"],
    moduleNameMapper: {
        "\\.(gif|ttf|eot|svg|png)$": "<rootDir>/__mocks__/image_mock.tsx",
        "\\.(css|less|sass|scss)$": "<rootDir>/__mocks__/styleMock.js",
        ".*SignalRManager.*$": "<rootDir>/__mocks__/signalR_mock.tsx",
    },
};
