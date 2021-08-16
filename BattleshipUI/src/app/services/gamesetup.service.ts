import { Injectable } from '@angular/core';
import { Socket } from 'ngx-socket-io';

@Injectable({
  providedIn: 'root'
})
export class GamesetupService {

  constructor(private socket: Socket) { }

  // create service variable
  roomCount = this.socket.fromEvent<number>('get gameboard');
}
