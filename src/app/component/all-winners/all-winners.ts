import {  Component, inject, OnInit, signal } from '@angular/core';
import { Winner } from '../../models/user.model';
import { RandomService } from '../../service/random.service';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-all-winners',
  imports: [TableModule],
  templateUrl: './all-winners.html',
  styleUrl: './all-winners.scss',
})
export class AllWinners implements OnInit{
    private randomService = inject(RandomService);
    allWinners = signal<Winner[]>([]);

      ngOnInit() {;
    this.loadWinner();
  }

  
  loadWinner() {
    this.randomService.getWinners().subscribe({
      next: (data) => {
      this.allWinners.set(data);
        // console.log('Winners report data:', data);
      },
      error: (err) => console.error('Error loading report:', err)
    });
  }

}
