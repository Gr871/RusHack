const path = require('path');

module.exports = function (app, connection) {
    /**
     * Видео
     */
    app.get('/get/video/1', function (req, res) {
        res.sendFile(path.resolve('src/video/1.mp4'));
    })
    app.get('/get/video/2', function (req, res) {
        res.sendFile(path.resolve('src/video/2.mp4'));
    })
    app.get('/get/video/3', function (req, res) {
        res.sendFile(path.resolve('src/video/3.mp4'));
    })
    app.get('/get/video/4', function (req, res) {
        res.sendFile(path.resolve('src/video/4.mp4'));
    })
    app.get('/get/video/5', function (req, res) {
        res.sendFile(path.resolve('src/video/5.mp4'));
    })
    app.get('/get/video/6', function (req, res) {
        res.sendFile(path.resolve('src/video/6.mp4'));
    })
    app.get('/get/video/7', function (req, res) {
        res.sendFile(path.resolve('src/video/7.mp4'));
    })
    app.get('/get/video/8', function (req, res) {
        res.sendFile(path.resolve('src/video/8.mp4'));
    })
    app.get('/get/video/9', function (req, res) {
        res.sendFile(path.resolve('src/video/9.mp4'));
    })
    app.get('/get/video/10', function (req, res) {
        res.sendFile(path.resolve('src/video/10.mp4'));
    })
    app.get('/get/video/11', function (req, res) {
        res.sendFile(path.resolve('src/video/11.mp4'));
    })
    app.get('/get/video/12', function (req, res) {
        res.sendFile(path.resolve('src/video/12.mp4'));
    })
    app.get('/get/video/13', function (req, res) {
        res.sendFile(path.resolve('src/video/13.mp4'));
    })
    app.get('/get/video/14', function (req, res) {
        res.sendFile(path.resolve('src/video/14.mp4'));
    })
    app.get('/get/video/15', function (req, res) {
        res.sendFile(path.resolve('src/video/15.mp4'));
    })
} 