import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface BookingCreateDto {
  hotelId: number;
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
  numberOfGuests: number;
  totalPrice: number;
  specialRequests?: string;
  guestEmail: string;
  guestName: string;
}

export interface BookingResponseDto {
  id: number;
  userId: number;
  hotelId: number;
  roomId: number;
  checkInDate: string;
  checkOutDate: string;
  numberOfGuests: number;
  totalPrice: number;
  status: string;
  specialRequests?: string;
  guestEmail: string;
  guestName: string;
  createdAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = 'https://localhost:7199/api/Booking';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  createBooking(dto: BookingCreateDto): Observable<any> {
    return this.http.post(this.apiUrl, dto, {
      headers: this.getHeaders()
    });
  }

  getMyBookings(): Observable<any> {
    return this.http.get(`${this.apiUrl}/my-bookings`, {
      headers: this.getHeaders()
    });
  }

  getBookingById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`, {
      headers: this.getHeaders()
    });
  }

  cancelBooking(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/cancel`, {}, {
      headers: this.getHeaders()
    });
  }
}