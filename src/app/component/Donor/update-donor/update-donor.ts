import { Component, effect, EventEmitter, inject, input, Input, Output, signal } from '@angular/core';
import { DonorService } from '../../../service/donor.service'; 
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { Donor } from '../../../models/donor.model';

@Component({
  selector: 'app-update-donor',
    standalone: true,
    imports: [ButtonModule, DialogModule, InputTextModule, FormsModule],  
    templateUrl: './update-donor.html',
  styleUrl: './update-donor.scss',

})
export class UpdateDonor {

private donorService = inject(DonorService);

  visible=signal(false);
@Output() donorSaved = new EventEmitter<void>();

 donor = {
    id: 0,
    firstName: '',
    lastName: '',
    eMail: ''
  };

donorTest=input<Donor | null>(null);


constructor() {
    effect(() => {
      const data = this.donorTest();
      if (data) {
        this.donor = { 
          id: data.id, 
          firstName: data.firstName, 
          lastName: data.lastName, 
          eMail: data.eMail 
        };
        this.visible.set(true);
      }
    });
  }



  saveUpdate() {
    this.donorService.updateDonor(this.donor).subscribe({
  next: (res) => {
    console.log('תורם עודכן בהצלחה', res);
    this.donorSaved.emit();
    this.visible.set(false);},
  error: (err) => console.error('שגיאה', err)
});
    console.log('מעדכן תורם:', this.donor);
    }
}
