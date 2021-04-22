insert into Users (NickName, Password) values
('Shian', '12345'), ('TNDRBLT', qwerty), ('Kuba_Rozruba', 54321);

insert into Chats (ChatName) values
('TestChat1'), ('TestGroupChat'), ('SHIAN-TNDRBLT');

insert into GroupChats (ChatId, UserId) values
(2,1), (2,2), (2,3);

insert into PrivateChats (ChatId, User1Id, User2Id) values
(1, 2, 3), (3, 1, 3);

insert into Messages ([Text], ChatId, UserId, MessageToReplyId) values
('Initial hello!', 2, 1, null),
('Hi', 3, 2, null),
('wasp', 2, 1, null),
('wassup', 2, 1, null);

insert into PrivatelyDeletedMessages (UserId, MessageId) values
(1, 3);