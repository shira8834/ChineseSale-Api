import { Component, inject } from '@angular/core';
import { RandomService } from '../../service/random.service'; 
import { Winner } from '../../models/user.model'; // אם יצרת Interface
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-random',
  imports: [FormsModule],
  templateUrl: './random.html',
  styleUrl: './random.scss',
})
export class Random {
  private randomService = inject(RandomService);

  
  onExecuteDraw(giftId: number) {
  this.randomService.runDraw(giftId).subscribe({
    next: (res) => {
      // כאן הקוד שמתבצע כשההגרלה הצליחה
      // this.winner = res;
      alert('ההגרלה בוצעה בהצלחה!');
    },
    error: (err) => {
      console.error('שגיאה בהגרלה:', err);
    }
  });
}
  
}
