import { Component, EventEmitter, inject, Output, signal } from '@angular/core';
import { DonorService } from '../../../service/donor.service'; 
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-donor',
  standalone: true,
  imports: [FormsModule, InputTextModule, ButtonModule,DialogModule],
  
  templateUrl: './add-donor.html',
  styleUrl: './add-donor.scss',
})
export class AddDonor {
private donorService = inject(DonorService);
private router = inject(Router); 

  visible=signal(false);
@Output() donorSaved = new EventEmitter<void>();

    showDialog() {
this.visible.set(true);
    }
   
  newDonor = {
    firstName: '',
    lastName: '',
    eMail: ''
  };

  saveDonor() {
    this.donorService.addDonor(this.newDonor).subscribe({
  next: (res) => {
    console.log('תורם נוסף בהצלחה', res);
    this.donorSaved.emit();
    this.visible.set(false);
    this.newDonor = { firstName: '', lastName: '', eMail: '' };
},
  error: (err) => console.error('שגיאה', err)
});
    console.log('שומר תורם:', this.newDonor);

  }
  
}
