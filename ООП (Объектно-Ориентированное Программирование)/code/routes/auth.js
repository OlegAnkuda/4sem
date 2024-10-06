const express = require('express')
const controller = require('../controllers/auth')
const router = express.Router()

//localhost:8200/api/auth/login
router.post('/login', controller.login)
//localhost:/api/auth/regist8200er
router.post('/register', controller.register)

module.exports = router