import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HotelService, HotelDto } from '../../../core/services/hotel.service';

@Component({
  selector: 'app-hotel-detail',
  templateUrl: './hotel-detail.component.html',
  styleUrls: ['./hotel-detail.component.css']
})
export class HotelDetailComponent implements OnInit {

  hotel: HotelDto | null = null;
  isLoading = false;
  hotelId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private hotelService: HotelService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.hotelId = Number(this.route.snapshot.paramMap.get('hotelId'));
    this.loadHotel();
  }

  loadHotel(): void {
    this.isLoading = true;
    this.hotelService.getHotelById(this.hotelId).subscribe({
      next: (data) => {
        this.hotel = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        this.snackBar.open(
          err.error?.message || 'Failed to load hotel.',
          'Close',
          { duration: 3000 }
        );
        this.router.navigate(['/hotels']);
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/hotels']);
  }

  getAmenitiesList(amenities: string): string[] {
    return amenities.split(',').map(a => a.trim());
  }

  bookRoom(roomId: number): void {
    this.router.navigate(['/booking'], {
      queryParams: { roomId: roomId }
    });
  }
}