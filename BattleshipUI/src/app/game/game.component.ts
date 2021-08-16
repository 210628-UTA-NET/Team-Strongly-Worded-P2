import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { GamesetupService } from '../services/gamesetup.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  fullRoom:Subscription;
  total:number;
  roomCount:Observable<number>;

  constructor(private gameSetupService: GamesetupService) { }

  ngOnInit() {
    this.fullRoom = this.gameSetupService.roomCount.subscribe(number => this.total = number)
  }

}
