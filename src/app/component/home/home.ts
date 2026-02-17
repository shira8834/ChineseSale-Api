import { Component,inject,OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { MenuItem } from 'primeng/api'; // ייבוא הטיפוס של הפריטים
import { Router, RouterModule } from '@angular/router';
import { DonorService } from '../../service/donor.service';
import { CommonModule } from '@angular/common';
import { Donor } from '../../models/donor.model'; // אם יש לך מודל
import { Observable } from 'rxjs';
import { Navbar } from '../navbar/navbar'; 

@Component({
  selector: 'app-home',
  imports: [ButtonModule, RouterModule,CommonModule,Navbar],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
constructor() {}
}
