import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BookingService } from '../services/booking.service';

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  bookingForm!: FormGroup;
  isLoading = false;
  errorMessage = '';
  minDate = new Date();

  constructor(
    private fb: FormBuilder,
    private bookingService: BookingService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.bookingForm = this.fb.group({
      hotelId: [1, Validators.required],
      roomId: [1, Validators.required],
      checkInDate: ['', Validators.required],
      checkOutDate: ['', Validators.required],
      numberOfGuests: [1, [Validators.required, Validators.min(1), Validators.max(10)]],
      totalPrice: [0, [Validators.required, Validators.min(1)]],
      specialRequests: [''],
      guestEmail: ['', [Validators.required, Validators.email]],
      guestName: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.bookingForm.invalid) return;

    this.isLoading = true;
    this.errorMessage = '';

    const formValue = this.bookingForm.value;
    const dto = {
      ...formValue,
      checkInDate: new Date(formValue.checkInDate).toISOString(),
      checkOutDate: new Date(formValue.checkOutDate).toISOString()
    };

    this.bookingService.createBooking(dto).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.router.navigate(['/booking/confirm'], {
          state: { booking: res.data }
        });
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Booking failed. Please try again.';
      }
    });
  }
}