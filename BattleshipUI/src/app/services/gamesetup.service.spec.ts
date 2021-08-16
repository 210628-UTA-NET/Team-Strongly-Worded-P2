import { TestBed } from '@angular/core/testing';

import { GamesetupService } from './gamesetup.service';

describe('GamesetupService', () => {
  let service: GamesetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GamesetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
