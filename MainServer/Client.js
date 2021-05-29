var clientIP = "";
var clientPORT = 0;

module.exports = function(ip, port){
    this.clientIP = ip;
    this.clientPORT = port;
}