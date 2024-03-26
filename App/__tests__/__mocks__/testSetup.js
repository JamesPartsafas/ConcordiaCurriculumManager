jest.mock("d3-selection", () => ({
    select: jest.fn(),
}));
jest.mock("d3-zoom", () => ({
    select: jest.fn(),
}));
jest.mock("uuid", () => ({
    select: jest.fn(),
}));
