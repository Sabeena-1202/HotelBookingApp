import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookingService, BookingResponseDto } from '../services/booking.service';

@Component({
  selector: 'app-booking-history',
  templateUrl: './booking-history.component.html',
  styleUrls: ['./booking-history.component.css']
})
export class BookingHistoryComponent implements OnInit {
  bookings: BookingResponseDto[] = [];
  isLoading = true;
  errorMessage = '';
  displayedColumns = [
    'id',
    'hotelId',
    'roomId',
    'checkInDate',
    'checkOutDate',
    'totalPrice',
    'status',
    'actions'
  ];

  constructor(
    private bookingService: BookingService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadBookings();
  }

  loadBookings(): void {
    this.isLoading = true;
    this.bookingService.getMyBookings().subscribe({
      next: (res) => {
        this.bookings = res.data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Failed to load bookings.';
        this.isLoading = false;
      }
    });
  }

  cancelBooking(id: number): void {
    if (!confirm('Are you sure you want to cancel this booking?')) return;

    this.bookingService.cancelBooking(id).subscribe({
      next: () => {
        this.loadBookings();
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Failed to cancel booking.';
      }
    });
  }

  goToBookingForm(): void {
    this.router.navigate(['/booking/form']);
  }
}