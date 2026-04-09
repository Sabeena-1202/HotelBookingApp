import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-booking-confirm',
  templateUrl: './booking-confirm.component.html',
  styleUrls: ['./booking-confirm.component.css']
})
export class BookingConfirmComponent implements OnInit {
  booking: any = null;

  constructor(private router: Router) {}

  ngOnInit(): void {
    const navigation = this.router.getCurrentNavigation();
    this.booking = navigation?.extras?.state?.['booking'];

    if (!this.booking) {
      this.router.navigate(['/booking/form']);
    }
  }

  goToHistory(): void {
    this.router.navigate(['/booking/history']);
  }

  goToHome(): void {
    this.router.navigate(['/']);
  }
}