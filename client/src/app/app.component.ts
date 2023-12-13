import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'client';
  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('http://localhost:5196/api/users').subscribe({
      next: (response) => {
        this.users = response;
      },
      error(err) {
        console.error(err);
      },
      complete: () => {
        console.log('Request Completed');
      },
    });
  }
}
