myMsgAdded = function (elem, id) {
    if (messageToDeleteId == 0 || messageToDeleteId != id) {
        messageToDeleteId = id;
        showDelete();
        redMsg.className = 'alert alert-primary';
        elem.className = 'alert alert-danger border border-danger';
        redMsg = elem;
        greenMsg.className = 'alert alert-dark';
    } else {
        messageToDeleteId = 0;
        showSend();
        elem.className = 'alert alert-primary';
    }
}

diffMsgAdded = function (elem, msg) {
    if (messageToReplyId == 0 || messageToReplyId != msg.id) {
        messageToReplyId = msg.id;
        textToReply = msg.text;
        userToReply = msg.user.nickName;
        showReply();
        redMsg.className = 'alert alert-primary';
        greenMsg.className = 'alert alert-dark';
        elem.className = 'alert alert-success border border-success';
        greenMsg = elem;
    } else {
        messageToReplyId = 0;
        textToReply = '';
        userToReply = '';
        showSend();
        elem.className = 'alert alert-dark';
    }
}