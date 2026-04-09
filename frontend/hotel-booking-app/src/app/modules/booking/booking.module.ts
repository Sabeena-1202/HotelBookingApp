import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BookingRoutingModule } from './booking-routing.module';

// Components
import { BookingFormComponent } from './booking-form/booking-form.component';
import { BookingConfirmComponent } from './booking-confirm/booking-confirm.component';
import { BookingHistoryComponent } from './booking-history/booking-history.component';

// Angular Material
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';

@NgModule({
  declarations: [
    BookingFormComponent,
    BookingConfirmComponent,
    BookingHistoryComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    BookingRoutingModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDividerModule
  ]
})
export class BookingModule { }