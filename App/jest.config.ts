export default {
    preset: "ts-jest",
    testEnvironment: "jest-environment-jsdom",
    testMatch: ["**/***.test.tsx"],
    moduleFileExtensions: ["js", "jsx", "tsx", "ts"],
    rootDir: "__tests__/",
    transform: {
        "^.+\\.tsx?$": [
            "ts-jest",
            {
                tsconfig: "./tsconfig-test.json",
                diagnostics: {
                    exclude: ["!*/.test.ts?(x)"],
                },
            },
        ],
    },
    moduleNameMapper: {
        "\\.(gif|ttf|eot|svg|png)$": "<rootDir>/__mocks__/image_mock.tsx",
        "\\.(css|less|sass|scss)$": "<rootDir>/__mocks__/styleMock.js",
    },
};
