var sendBtn = document.getElementById('sendBtn');
var replyBtn = document.getElementById('replyBtn');
var replyPrivBtn = document.getElementById('replyPrivBtn');
var editBtn = document.getElementById('editBtn');
var deleteBtn = document.getElementById('deleteBtn');
var deletePrivBtn = document.getElementById('deletePrivBtn');

var form = document.getElementById('inputForm');

function init() {
    form.removeChild(replyBtn);
    form.removeChild(replyPrivBtn);
    form.removeChild(editBtn);
    form.removeChild(deleteBtn);
    form.removeChild(deletePrivBtn);
}

function showReply() {
    if (form.querySelector('#sendBtn')) form.removeChild(sendBtn);
    if (form.querySelector('#editBtn')) form.removeChild(editBtn);
    if (form.querySelector('#deleteBtn')) form.removeChild(deleteBtn);
    if (form.querySelector('#deletePrivBtn')) form.removeChild(deletePrivBtn);
    form.appendChild(replyBtn);
    form.appendChild(replyPrivBtn);
}

function showSend() {
    if (form.querySelector('#replyBtn')) form.removeChild(replyBtn);
    if (form.querySelector('#replyPrivBtn')) form.removeChild(replyPrivBtn);
    if (form.querySelector('#editBtn')) form.removeChild(editBtn);
    if (form.querySelector('#deleteBtn')) form.removeChild(deleteBtn);
    if (form.querySelector('#deletePrivBtn')) form.removeChild(deletePrivBtn);
    form.appendChild(sendBtn);
}

function showDelete() {
    if (form.querySelector('#sendBtn')) form.removeChild(sendBtn);
    if (form.querySelector('#replyBtn')) form.removeChild(replyBtn);
    if (form.querySelector('#replyPrivBtn')) form.removeChild(replyPrivBtn);
    form.appendChild(editBtn);
    form.appendChild(deleteBtn);
    form.appendChild(deletePrivBtn);
}

init();