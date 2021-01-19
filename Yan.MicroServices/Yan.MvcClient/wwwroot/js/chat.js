var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();
connection.keepAliveIntervalInMilliseconds = 12e4;
connection.serverTimeoutInMilliseconds = 24e4;

connection.on("AddNewMessage", function (user, message) {
    console.log(user + " 留言 " + message);
});

connection.start();