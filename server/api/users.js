module.exports = function (app, connection) {
    /**
    * Регистрация пользователя
    */
    app.post('/api/add/users/', function (req, res) {
        var sql = 'INSERT INTO users (id_users, logn_users, password_users, first_name_users, last_name_users, email_users, data_birth_users, country_users, interessant_users, foto_users, users_positions, users_company, language_users) VALUES (?)';
        // Описание переменных
        const login = req.body.user;
        const password = req.body.pass;
        const name = req.body.firstName;
        const secondname = req.body.lastName;
        const email = req.body.email;
        const age = req.body.dateOfBirth;
        const company = req.body.company
        const position = req.body.position
        const country = req.body.country;
        const language = req.body.language;
        const interessant = req.body.hobbies;
        const photo = Buffer.from(req.body.profileImage, 'base64');

        var values = [null, login, password, name, secondname, email, age, country, interessant, photo, position, company, language];
        try {
            connection.query(sql, [values], function (err, data) {
                if (err) return res.send(err.sqlMessage);
                res.send(data)
            })
        } catch (error) {
            error;
        }
    })

    /*   
    * Авторизация пользователя
    */
    app.post('/api/find/users/', function (req, res) {
        // Описание переменных
        const login = req.body.user;
        const password = req.body.pass;
        var sql = `SELECT id_users, first_name_users, last_name_users, email_users, data_birth_users, country_users, interessant_users, users_positions, users_company, language_users FROM users WHERE ( users.logn_users LIKE '${login}') AND (password_users LIKE '${password}')`;
        connection.query(sql, function (err, data) {
            if (err) return res.send(err.errno);
            res.send(data);
        })
    })
}
