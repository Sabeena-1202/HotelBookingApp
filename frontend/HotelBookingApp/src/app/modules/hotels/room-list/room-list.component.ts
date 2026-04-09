import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { RoomDto } from '../../../core/services/hotel.service';

@Component({
  selector: 'app-room-list',
  templateUrl: './room-list.component.html',
  styleUrls: ['./room-list.component.css']
})
export class RoomListComponent {

  @Input() rooms: RoomDto[] = [];
  @Output() bookRoom = new EventEmitter<number>();

  constructor(private router: Router) {}

  onBookRoom(roomId: number): void {
    this.bookRoom.emit(roomId);
  }
}