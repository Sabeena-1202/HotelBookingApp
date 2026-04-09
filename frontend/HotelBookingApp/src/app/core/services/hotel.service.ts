import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RoomDto {
  roomId: number;
  roomNumber: string;
  type: string;
  description: string;
  pricePerNight: number;
  maxGuests: number;
  isAvailable: boolean;
  hotelId: number;
}

export interface HotelDto {
  hotelId: number;
  name: string;
  location: string;
  description: string;
  amenities: string;
  rating: number;
  rooms: RoomDto[];
}

@Injectable({
  providedIn: 'root'
})
export class HotelService {

  private apiUrl = 'https://localhost:7055/api/hotel';

  constructor(private http: HttpClient) {}

  getAllHotels(): Observable<HotelDto[]> {
    return this.http.get<HotelDto[]>(`${this.apiUrl}/all`);
  }

  searchHotels(
    location?: string,
    minPrice?: number,
    maxPrice?: number,
    amenity?: string
  ): Observable<HotelDto[]> {
    let params = new HttpParams();

    if (location) params = params.set('location', location);
    if (minPrice) params = params.set('minPrice', minPrice.toString());
    if (maxPrice) params = params.set('maxPrice', maxPrice.toString());
    if (amenity) params = params.set('amenity', amenity);

    return this.http.get<HotelDto[]>(`${this.apiUrl}/search`, { params });
  }

  getHotelById(hotelId: number): Observable<HotelDto> {
    return this.http.get<HotelDto>(`${this.apiUrl}/${hotelId}`);
  }

  getRoomsByHotelId(hotelId: number): Observable<RoomDto[]> {
    return this.http.get<RoomDto[]>(`${this.apiUrl}/${hotelId}/rooms`);
  }

  getRoomById(roomId: number): Observable<RoomDto> {
    return this.http.get<RoomDto>(`${this.apiUrl}/room/${roomId}`);
  }
}