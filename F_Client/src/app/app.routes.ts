import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { OlympicGridComponent } from './components/olympic-grid/olympic-grid.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'olympic-grid', component: OlympicGridComponent }
];
