var path = require("path");
var webpack = require("webpack");

module.exports = {
  entry: "./bin/canvas",
  output: {
      path: path.join(__dirname, "public"),
      filename: "bundle.js",
  },
  devServer: {
    contentBase: path.join(__dirname, "public"),
    port: 8080
  }
}
