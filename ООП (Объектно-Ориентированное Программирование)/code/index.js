const app = require('./app')
const port = process.env.PORT || 8200 // process.env.PORT for changes from console or 8200 by default

app.listen(port, () => console.log(`Server has been started on ${port}`))