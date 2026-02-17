import { Component, inject, signal, OnInit, computed } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { Router } from '@angular/router';
import { BagService} from '../../../service/bag.service';
import { GetBagDto, CreateBagDto } from '../../../models/bag.model';

@Component({
  selector: 'app-bag',
  standalone: true,
  imports: [TableModule, CommonModule, ButtonModule],
  templateUrl: './bag-list.html',
  styleUrl: './bag-list.scss',
})
export class Baglist implements OnInit {
  private bagService = inject(BagService);
  private router = inject(Router);

  // שימוש בסיגנל עבור רשימת הסל
  bag = signal<any[]>([]);

  // חישוב סכום סופי
  totalPrice = computed(() => {
    return this.bag().reduce((sum, item) => sum + (item.gift?.price || 0) * (item.quantity || 0), 0);
  });

  ngOnInit() {
    this.initUserBag();
  }

  private initUserBag() {
    const userString = localStorage.getItem('user');
    if (!userString) {
      this.router.navigate(['/login']);
      return;
    }

    const storedData = JSON.parse(userString);
    const userId = storedData.user?.id || storedData.user?.Id || storedData.id;

    if (userId) {
      this.loadBag(userId);
    } else {
      this.router.navigate(['/login']);
    }
  }

  loadBag(userId: number) {
    this.bagService.getBagsByUserId(userId).subscribe({
      next: (data) => this.bag.set(data),
      error: (err) => console.error('טעינת הסל נכשלה:', err)
    });
  }

  plusQuent(item: any) {
    // 1. חילוץ המזהים בצורה בטוחה (תומך גם ב-i וגם ב-I)
    const userId = item.idUser || item.IdUser;
    const giftId = item.idGift || item.IdGift;
  
    const payload: CreateBagDto = {
      idUser: Number(userId),
      idGift: Number(giftId),
      quantity: 1
    };
  
    this.bagService.addBag(payload).subscribe({
      next: () => {
        // 2. עדכון ה-Signal בצורה אגרסיבית
        this.bag.update(currentBag => {
          return currentBag.map(i => {
            // השוואת מזהים גמישה
            const currentItemId = i.idGift || i.IdGift;
            const targetId = giftId;
  
            if (currentItemId === targetId) {
              // יצירת אובייקט חדש לגמרי כדי ש-Angular יזהה שינוי
              return {
                ...i,
                quantity: (Number(i.quantity) || 0) + 1,
                Quantity: (Number(i.Quantity) || 0) + 1 // עדכון שני המופעים ליתר ביטחון
              };
            }
            return i;
          });
        });
        console.log('התצוגה עודכנה מקומית');
      },
      error: (err) => {
        console.error('שגיאה בשרת:', err);
        alert('לא ניתן לעדכן את הכמות כרגע');
      }
    });
  }

  minusQuent(item: any) {
    // 1. חילוץ מזהים
    const userId = item.idUser || item.IdUser;
    const giftId = item.idGift || item.IdGift;
  
    // 2. בדיקה: אם הכמות היא 1, לא עושים כלום (כדי שלא ירד ל-0)
    const currentQty = Number(item.quantity || item.Quantity || 0);
    if (currentQty <= 1) return;
  
    const payload: CreateBagDto = {
      idUser: Number(userId),
      idGift: Number(giftId),
      quantity: -1 // שולחים מינוס 1 לשרת כדי שיוריד מהקיים
    };
  
    this.bagService.addBag(payload).subscribe({
      next: () => {
        // 3. עדכון ה-Signal - מורידים בדיוק 1
        this.bag.update(currentBag => {
          return currentBag.map(i => {
            const currentItemId = i.idGift || i.IdGift;
            if (currentItemId === giftId) {
              return {
                ...i,
                quantity: currentQty - 1,
                Quantity: currentQty - 1
              };
            }
            return i;
          });
        });
        console.log('הורדנו אחד בהצלחה');
      },
      error: (err) => {
        console.error('שגיאה בעדכון:', err);
        alert('לא ניתן לעדכן את הכמות');
      }
    });
  }

  
  deleteBag(id: number) {
    this.bagService.deleteBag(id).subscribe({
      next: () => {
        this.bag.update(prev => prev.filter(item => (item.id || item.Id) !== id));
      },
      error: (err) => console.error('מחיקה נכשלה:', err)
    });
  }

processPurchase() {
  const userString = localStorage.getItem('user');
  if (!userString) return;

  const storedData = JSON.parse(userString);
  const userId = storedData.user?.id || storedData.user?.Id || storedData.id;

  if (this.bag().length === 0) {
    alert('הסל שלך ריק, אין מה לרכוש');
    return;
  }

  this.bagService.ProcessCheckout(userId).subscribe({
    next: (response) => {
      alert('הרכישה בוצעה בהצלחה!');
      this.bag.set([]); // מרוקנים את הסל במסך אחרי הרכישה
      this.router.navigate(['/orders']); // אופציונלי: מעבר לדף הזמנות
    },
    error: (err) => {
      console.error('Purchase failed:', err);
      alert('חלה שגיאה בביצוע הרכישה');
    }
  });
}  
}