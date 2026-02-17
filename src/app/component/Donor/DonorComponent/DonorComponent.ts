import { Component, OnInit, inject, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { DonorService } from '../../../service/donor.service';
import { Donor } from '../../../models/donor.model';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { AddDonor } from '../add-donor/add-donor';
import { UpdateDonor } from '../update-donor/update-donor';
import { AccordionModule } from 'primeng/accordion';
import { Navbar } from '../../navbar/navbar'; 
import { RouterOutlet } from '@angular/router';



@Component({
  selector: 'app-donor',
  standalone: true,
  imports: [TableModule,CommonModule,ButtonModule,AddDonor,UpdateDonor,AccordionModule,Navbar,RouterOutlet],
  providers: [DonorService],
  templateUrl: './DonorComponent.html',
  styleUrl: './DonorComponent.scss',
})
export class DonorComponent  implements OnInit {
   tabs = [
  { title: 'מידע נוסף', value: '0', content: 'תוכן כלשהו...' },
  { title: 'עזרה', value: '1', content: 'תוכן אחר...' }
];

  private donorService = inject(DonorService);
  donors = signal<Donor[]>([]);           
  // filteredDonors = signal<Donor[]>([]);

  ngOnInit() {
  this.loadDonors();
}
 
    loadDonors() {
    this.donorService.donorWithGifts().subscribe({
      next: (data) => {
      this.donors.set(data);
      // this.filteredDonors.set(data);
      },
      error: (err) => console.error('שגיאה בטעינת תורמים:', err)
    });
      }

    deleteDonor(id: number) {
    this.donorService.deleteDonor(id).subscribe({
      next: () => {
        console.log(`Donor with id ${id} deleted.`);
        this.donors.update(curr => curr.filter(d => d.id !== id)?? []);
        // this.filteredDonors.update(curr => curr?.filter(d => d.id !== id) ?? []);  
          },
      error: (err) => console.error('Error deleting donor:', err)
    });
  }
selectedDonor = signal<Donor | null>(null);
  openUpdate(donor: Donor) {
    this.selectedDonor.set(donor);
    }

  // onDonorSaved() {
  //   this.selectedDonor.set(null);
  // }

  }
