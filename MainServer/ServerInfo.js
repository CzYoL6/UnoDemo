var ip = "";
var port = 0;
var maxPlayer = 0;
var onlinePlayer = 0;
var name = "";

module.exports = function(ip, port, mp, op, name){
    this.ip = ip;
    this.port = port;
    this.maxPlayer = mp;
    this.onlinePlayer = op
    this.name = name;
}