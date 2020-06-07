const express = require('express');
const app = express();
const mysql = require('mysql');
const bodyParser = require("body-parser");
const cors = require('cors');
const http = require('http').Server(app);
const io = require('socket.io')({
  transports: ['websocket']
});
const _room = require('./functions/roomsActions');

const port = 5000
const host = '10.13.4.52';
const connection = mysql.createConnection({
  host: "localhost",
  user: "root",
  database: "hakaton",
  password: ""
});

//app.use
app.use(bodyParser.urlencoded({ limit: "50mb", extended: true, parameterLimit: 50000 }))
app.use(bodyParser.json())
app.use(cors())

//Socket.IO
io.attach(4567);
io.on('connection', function (socket) {
  console.log(`Connection ${new Date()} and id: ${socket.id}`);

  //Прием сообщения и отправка в комнату
  socket.on("reqMes", function (arr) {
    // Запись сообщения в БД (необходимо указать текст, имя комнаты, логин пользователя)
    _room.writeSmsUser(connection, arr, function (callback) { })
    // Придет сообщение и комната 
    io.in(arr.room).emit('message', { sms: arr.mess, user: arr.login });
  })

  // Список пользователей
  socket.on('send', function () {
    _room.returnUser(connection, function (callback) {
      io.in(socket.id).emit('loginUsers', { loginUsers: callback })
    })
  })

  // Список доступных комнат 
  socket.on('sendCanals', function () {
    _room.getRooms(connection, function (callback) {
      io.in(socket.id).emit('canals', { canals: callback })
    })
  })

  // Список комнат пользователя
  socket.on('saveRoom', function (arr) {
    _room.readRoomsUser(connection, arr, function (callback) {
      io.in(socket.id).emit('saveRoomsForUser', { saveRooms: callback })
    })
  })

  //Подключение к комнате
  socket.on('room', function (arr) {
    socket.join(arr.room);

    // Получем все сообщения из канала, при подсоединении пользователя загружаем)
    _room.readSmsRoom(connection, arr, function (callback) {
      io.in(socket.id).emit('saveSms', { saveSms: callback });
    });

    // В случае отсутствия комнаты для данных пользователей => создаем  
    _room.addRoom(connection, arr, function (callback) { })

    // Если пользователь первый раз заходит в данную комнату, то записываем комнату в комнаты пользователя
    _room.addRoomForUser(connection, arr, function (callback) { })

  })

  // Пользователь выходит из комнаты
  socket.on('delete', function (arr) {
    io.in(arr.room).emit('out', { user: login });
    _room.deleteRoomUser(connection, arr.login, function () { })
  })

  // Создание персональной комнаты
  socket.on('personalRoom', function (arr) {
    // Создаем уникальное имя комнаты
    var nameRoom = _room.createNamePersonalRoom(arr.login1, arr.login2);
    //  Тестовый режим!
    arr.descriptionRoom = ' ';
    arr.room = nameRoom;
    arr.canalCardStaus = 1;
    arr.descriptionRoom = 'PersonalUsersChat';

    // Создаем персональную комнату
    _room.addRoom(connection, arr, function () { })

    // Добавляем инициатора T&T в комнату
    _room.addRoomForUser(connection, arr, function (callback) { })
    socket.join(nameRoom);

    // Отправляем имя созданной комнаты клиенту
    io.in(nameRoom).emit('create', { create: arr.room });
  })

})

// Описание API
app.get('/', (req, res) => res.sendFile('src/html/about.html', { root: __dirname }))

app.listen(port, host, function () {
  console.log(`App listening on host: ${host} and port ${port}! DateTime: ${new Date()}`);
  //API
  require('./api/users')(app, connection);
  require('./api/test')(app, connection);
  require('./api/canals')(app, connection);

})