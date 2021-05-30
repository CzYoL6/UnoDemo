const express = require('express');
const bodyParser = require('body-parser');
const serverInfo = require('./ServerInfo');
const { json } = require('express');

const app = new express();

const version = 1.2;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

var servers = {};
var clients = {};

app.post('/RegisterGameServerInfo', (req, res, next) => {
    var key = req.body.ip + "-" + req.body.port;
    servers[key] = new serverInfo(req.body.ip,req.body.port,req.body.maxPlayer, 0, req.body.name);
    console.log("successfully registered: ");
    for(var x in servers){
        console.log(servers[x].ip+":"+servers[x].port+" "+servers[x].maxPlayer+" "+servers[x].onlinePlayer+" "+ servers[x].name + "\n");
    }
    return res.status(200).json({
        title: "successfully registered",
    });
});

app.post('/UpdateGameServerInfo', (req, res, next) => {
    var key = req.body.ip + "-" + req.body.port;
    servers[key].onlinePlayer = req.body.onlinePlayer;
    console.log("successfully updated: ");
    for(var x in servers){
        console.log(servers[x].ip+":"+servers[x].port+" "+servers[x].maxPlayer+" "+servers[x].onlinePlayer+" "+ servers[x].name +"\n");
    }
    return res.status(200).json({
        title: "successfully updated",
    });
});

app.post('/UnregisterGameServerInfo', (req, res, next) => {
    var key = req.body.ip + "-" + req.body.port;
    delete servers[key];
    console.log("successfully unregistered: ");
    for(var x in servers){
        console.log(servers[x].ip+":"+servers[x].port+" "+servers[x].maxPlayer+" "+servers[x].onlinePlayer+" "+ servers[x].name +"\n");
    }
    return res.status(200).json({
        title: "successfully deleted",
    });
});

app.post('/GetServerInfos', (req, res, next) => {
    console.log(req.body.version);
    console.log(version);
    if(req.body.version != version) {
        console.log('need update');
        return res.status(200).json({
            title: "please update",
            serverInfo: []
        });
    }
    var serverInfos = [];
    for(var x in servers){
        serverInfos.push({
            "ip": servers[x].ip,
            "port":servers[x].port,
            "maxPlayer":servers[x].maxPlayer,
            "onlinePlayer":servers[x].onlinePlayer,
            "name" : servers[x].name
        });
    }
    return res.status(200).json({
        title: "successfully get",
        serverInfo : serverInfos
    });
});



const port = process.env.PORT || 5000;

app.listen(port, (err => {
    if (err) return console.log(err);
    console.log("MainServer running at " + port);
}));