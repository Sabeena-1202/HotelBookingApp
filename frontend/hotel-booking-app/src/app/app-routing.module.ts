import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'booking',
    loadChildren: () =>
      import('./modules/booking/booking.module').then(m => m.BookingModule)
  },
  { path: '', redirectTo: '/booking/form', pathMatch: 'full' },
  { path: '**', redirectTo: '/booking/form' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }