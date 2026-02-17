import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { GetOrderDto } from '../../../models/order.model';
import { OrderService } from '../../../service/order.service.'; 

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule, TableModule],
  templateUrl: './order-list.html',
  styleUrl: './order-list.scss'
})
export class OrderList implements OnInit {
  private orderService = inject(OrderService);
  
  orders = signal<GetOrderDto[]>([]);

  ngOnInit(): void {
    this.loadAllOrders();
  }

  loadAllOrders(): void {
    this.orderService.GetAllOrders().subscribe({
      next: (data) => {
        // console.log('הזמנות הגיעו:', data);
        this.orders.set(data); 
      },
      error: (err) => {
        console.error('שגיאה בטעינת הזמנות:', err);
      }
    });
  }


  // 
  totalRevenue = computed(() => {
    return this.orders().reduce((sum, order) => sum + (order.gift?.price || 0), 0);
  });

  // חישוב ממוצע להזמנה 
  averageOrderValue = computed(() => {
    const count = this.orders().length;
    return count > 0 ? this.totalRevenue() / count : 0;
  });
}