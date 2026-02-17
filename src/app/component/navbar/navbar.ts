import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, MenubarModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar implements OnInit{
items: MenuItem[] | undefined;

  ngOnInit() {
    this.items = [
      {
        label: 'דף הבית',
        icon: 'pi pi-home',
        routerLink: '/'
      },
      {
        label: 'תורמים',
        icon: 'pi pi-users',
        routerLink: '/donor'
      },
      {
        label: 'מתנות',
        icon: 'pi pi-gift',
        routerLink: '/gift' 
      },
        {
        label: 'קטגוריות',
        icon: 'pi pi-tags',
        routerLink: '/category' 
      },
      {
        label: 'הגדרות',
        icon: 'pi pi-cog'
      },
      {
        label: 'התנתק',
        icon: 'pi pi-sign-out',
        routerLink: '/login'
      },
      {
        label: 'התחבר',
        icon: 'pi pi-sign-in',
        routerLink: '/login'
      },
      {
       icon:'pi pi-shopping-cart',
       routerLink:'/bag'
      }
    ];
  }
}
