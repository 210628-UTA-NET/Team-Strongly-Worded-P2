import { Injectable } from '@angular/core';
import { Socket } from 'ngx-socket-io';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  message: string;
  broadcast:string;
  messages = this.socket.fromEvent<string[]>('get message');

  // initialize socket object
  constructor(private socket: Socket) { }

  sendMessage(message: string) {
    this.socket.emit('message', message);
  }
}
