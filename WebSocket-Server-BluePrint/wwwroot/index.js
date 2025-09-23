const connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

document.getElementById("joinBtn").addEventListener("click", joinChannel);
document.getElementById("sendBtn").addEventListener("click", sendMessage);
connection.on("ReceiveMessage", (user, message) => {
    console.log(message);
    printMessage(user, message)
})

async function joinChannel() {
    const channel = document.getElementById("channel").value;
    await connection.invoke("JoinChannel", channel);
}
function printMessage(user, message) {
    const li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    document.getElementById("messageList").appendChild(li);
}
async function sendMessage() {
    const channel = document.getElementById("channel").value;
    const message = document.getElementById("message").value;
    const user = document.getElementById("user").value;
    printMessage(user, message);
    await connection.invoke("SendMessageToChannel", channel, user, message );
}
connection.start();