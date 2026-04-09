import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HotelService, HotelDto } from '../../../core/services/hotel.service';

@Component({
  selector: 'app-hotel-list',
  templateUrl: './hotel-list.component.html',
  styleUrls: ['./hotel-list.component.css']
})
export class HotelListComponent implements OnInit {

  hotels: HotelDto[] = [];
  isLoading = false;
  searchForm: FormGroup;

  constructor(
    private hotelService: HotelService,
    private fb: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.searchForm = this.fb.group({
      location: [''],
      minPrice: [''],
      maxPrice: [''],
      amenity: ['']
    });
  }

  ngOnInit(): void {
    this.loadAllHotels();
  }

  loadAllHotels(): void {
    this.isLoading = true;
    this.hotelService.getAllHotels().subscribe({
      next: (data) => {
        this.hotels = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        this.snackBar.open(
          err.error?.message || 'Failed to load hotels.',
          'Close',
          { duration: 3000 }
        );
      }
    });
  }

  onSearch(): void {
    this.isLoading = true;
    const { location, minPrice, maxPrice, amenity } = this.searchForm.value;

    this.hotelService.searchHotels(
      location || undefined,
      minPrice || undefined,
      maxPrice || undefined,
      amenity || undefined
    ).subscribe({
      next: (data) => {
        this.hotels = data;
        this.isLoading = false;
        if (data.length === 0) {
          this.snackBar.open(
            'No hotels found for your search.',
            'Close',
            { duration: 3000 }
          );
        }
      },
      error: (err) => {
        this.isLoading = false;
        this.snackBar.open(
          err.error?.message || 'Search failed.',
          'Close',
          { duration: 3000 }
        );
      }
    });
  }

  onReset(): void {
    this.searchForm.reset();
    this.loadAllHotels();
  }

  viewHotel(hotelId: number): void {
    this.router.navigate(['/hotels', hotelId]);
  }

  getAmenitiesList(amenities: string): string[] {
    return amenities.split(',').map(a => a.trim());
  }
}