module.exports = {

    // Добавление комнаты
    addRoom: function (connection, arr, callback) {
        arr.private = 1;
        arr.descriptionRoom = 1;
        arr.canalCardStaus = 1;
        var sql = `INSERT INTO canal (name_canal, description_canal, canal_card_status, id_canals, canal_private)  VALUES ('${arr.room}', '${arr.descriptionRoom}', '${arr.canalCardStaus}', NULL, '${arr.private}')`;
        connection.query(sql, function (err, data) {
            if (err) return callback(arr.room);
            return callback(data);
        })
    },

    // Добавление пользователя в комнату
    addRoomForUser: function (connection, arr, callback) {
        // console.log(arr);
        var sqlSearch = `SELECT canal_insert_user.canal_insert_user_id  FROM canal_insert_user WHERE (canal_insert_id_canal LIKE '${arr.room}') AND (canal_insert_user.canal_insert_user_id LIKE '${arr.login}')`;
        connection.query(sqlSearch, function (err, search) {
            if (err) return console.log(err);
            try {
                search[0].canal_insert_user_id
            } catch (error) {
                var sql = `INSERT INTO canal_insert_user (canal_insert_user_id, canal_insert_id_canal) VALUES ('${arr.login}', '${arr.room}')`;
                connection.query(sql, function (err, data) {
                    if (err) return console.log(err.sqlMessage);
                    return callback(data)
                })
            }
        })
    }, 

    // Удаление комнаты у пользователя
    deleteRoomUser: function (connection, login, callback) {
        var sql = `DELETE FROM canal_insert_user WHERE canal_insert_user_id LIKE '${login}`;
        connection.query(sql, function (err, data) {
            if (err) return console.log(err.sqlMessage);
            return callback(data);
        })
    },

    // Запись сообщения пользователя в БД
    writeSmsUser: function (connection, arr, callback) {
        var sql = `INSERT INTO sms (id_sms, text_sms, canals_sms, users_sms, media_sms) VALUES (NULL, '${arr.mess}', '${arr.room}', '${arr.login}', NULL)`;
        connection.query(sql, function (err, data) {
            if (err) return console.log(err.sqlMessage);
            return callback(data);
        })
    },

    // Получить все сообщения комнаты
    readSmsRoom: function (connection, arr, callback) {
        var sql = `SELECT sms.text_sms, sms.users_sms FROM sms WHERE (canals_sms LIKE '${arr.room}') `;
        connection.query(sql, function (err, data) {
            if (err) return console.log(err.sqlMessage);
            return callback(data);
        })
    },

    // Создание персональной комнаты
    createNamePersonalRoom: function(login1, login2){
        return namePersonalRoom = login1 + ' and ' + login2;
    },

    // Получить все комнаты в которых участвует пользователь
    readRoomsUser: function(connection, arr, callback) {
        var sql = `SELECT canal_insert_id_canal FROM canal_insert_user WHERE canal_insert_user.canal_insert_user_id LIKE '${arr.login}'`;
        connection.query(sql, function (err, data) {
            if (err) return console.log(err.sqlMessage);
            return callback(data);
        })
    },

    // Получить все логины пользователей
    returnUser: function(connection, callback) {
        var sql = `SELECT logn_users FROM users`;
        connection.query(sql, function (err, data) {
            if (err) return console.log(err.sqlMessage);
            return callback(data);
        })
    },

    // Получить все комнаты
    getRooms: function(connection, callback) {
        var sql = `SELECT name_canal FROM canal`;
        connection.query(sql, function (err, data) {
            if (err) return console.log(err.sqlMessage);
            return callback(data);
        })
    }
}