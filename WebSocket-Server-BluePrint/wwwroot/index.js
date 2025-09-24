
/* Vi starter her ved å sette opp en ny connection mot vår chathub. */
const connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

/* Vi setter også opp en referanse til channelName vi kan bruke, istedenfor å alltid lese html.*/
let channelName = "";

/* siden vår js fil er en modul, og blir defer loaded, må vi hente inn,
og sette eventlisteners etter html er loaded. */
document.getElementById("joinBtn").addEventListener("click", joinChannel);
document.getElementById("sendBtn").addEventListener("click", sendMessage);

/* Her setter vi opp en lytter mot ReceiveMessage eventen vi tilgjengeliggjør i hubben vår. */
connection.on("ReceiveMessage", (user, message) => {
    printMessage(user, message)
})

/* Her definerer vi en asynkron funksjon, som invoker vår JoinChannel method på hubben på serveren vår. */
async function joinChannel() {
    const channel = document.getElementById("channel").value;
    channelName = channel;
    await connection.invoke("JoinChannel", channel);
    /* Vi legger også channelnavnet inn i channelOutput*/
    document.getElementById("channelOutput").textContent = channelName;
}

/* Dette er en hjelpefunksjon som printer en melding til message listen vår.*/
function printMessage(user, message) {
    const li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    document.getElementById("messageList").appendChild(li);
}

/* Dette er en asynkron funksjon som sender en melding til vår chathub, ved å invoke SendMessageToChannel metoden i vår chathub.*/
async function sendMessage() {
    const channel = channelName.length === 0 ? document.getElementById("channel").value : channelName;
    const message = document.getElementById("message").value;
    const user = document.getElementById("user").value;
    printMessage(user, message);
    await connection.invoke("SendMessageToChannel", channel, user, message );
}
connection.start();