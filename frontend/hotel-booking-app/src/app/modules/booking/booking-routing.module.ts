import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookingFormComponent } from './booking-form/booking-form.component';
import { BookingConfirmComponent } from './booking-confirm/booking-confirm.component';
import { BookingHistoryComponent } from './booking-history/booking-history.component';

const routes: Routes = [
  { path: 'form', component: BookingFormComponent },
  { path: 'confirm', component: BookingConfirmComponent },
  { path: 'history', component: BookingHistoryComponent },
  { path: '', redirectTo: 'form', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookingRoutingModule { }