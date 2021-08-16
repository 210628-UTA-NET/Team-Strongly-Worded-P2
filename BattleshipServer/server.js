// creates an express object
const app = require('express')();
// creates a server using http?
const http = require('http').Server(app);
// connects socketio with express server?
// add cors
const io = require('socket.io')(http, {
    cors: {
        origin:'http://localhost:4200',
        methods: ["GET", "POST"],
        transports: ['websocket', 'polling', 'flashsocket'],
        credentials: true
    }
});

// create a collection of rooms
const rooms = {};
// collection of messages
const messages = [];

// logic for when a socket connects to the server
// server event listener
io.on('connection', socket => {
    let previousRoomId;

    // create a way to joins rooms
    const safeJoin = currentRoomId => {
        // leave previous room
        socket.leave(previousRoomId);
        // join new room with debugging message to console
        socket.join(currentRoomId, () => console.log(`Socket ${socket.id} joined room ${currentRoomId}`));
        // keep track of current room
        previousRoomId = currentRoomId;
    };

    // logic for when a socket tries to join a room
    socket.on('join room', roomId => {
        let roomCount = io.nsps['/'].adapter.rooms[roomId];
        if(!roomCount) {
            console.log("Room count variable is null for a reason");
        }
        else {
            if(Object.keys(roomCount).length == 2) {
                console.log('Room has 2 people');
                socket.to(roomId).emit('get gameboard', 2);
            }
            else {
                safeJoin(roomId);
                console.log('Room count is now 1');
                // joining socket will know this one joined a room?
                socket.emit('room', rooms[roomId]);
            }
        } 
    });

    // logic for when a socket wants to add a room to the room list
    socket.on('add a room', room => {
        rooms[room.id] = room;
        safeJoin(room.id);
        // io.emit broadcasts to all clients, not just the acting socket
        io.emit('rooms', Object.keys(rooms));
        socket.emit('room', room);
    });

    // logic for when a socket sends a message in chat
    socket.on('message', msg => {
        messages[msg] = msg;
        //console.log(`Message on the server is ${messages[msg.id]}`);
        console.log(messages);
        io.emit('get message', Object.keys(messages));
        socket.emit('see message', msg);
    });

    // broadcast call rooms and sockets that have connected
    io.emit('rooms', Object.keys(rooms));

    console.log(`Socket ${socket.id} has connected`);
});

// broadcast server to a port so others can listen in
http.listen(3000, () => {
    console.log('Listening on port 3000');
});