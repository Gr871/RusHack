module.exports = function (app, connection) {
    /**
     * Добавление канала
    */
    app.post('/api/add/canal/', function (req, res) {
        var sql = "INSERT INTO canal (name_canal, description_canal, id_canals) VALUES (?);";
        const name = req.body.name;
        const description = req.body.description;
        var value = [name, description];
        connection.query(sql, [value], function (err, data) {
            if (err) return console.log(err);
            res.send(data)
        })
    })

    /**
    * Добавление пользователя к каналу
    */
    app.post('/api/add/canal/user', function (req, res) {
        var sql = "INSERT INTO canal_insert_user (canal_insert_user_id, canal_insert_id_canal) VALUES(?)";
        // Описание переменных
        const login = req.body.login;
        const nameRoom = req.body.nameRoom;
        var value = [login, nameRoom];
        connection.query(sql, [value], function (err, data) {
            if (err) return console.log(err);
            res.send(data)
        })
    })
}
