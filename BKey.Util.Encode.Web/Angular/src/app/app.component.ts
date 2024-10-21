import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EncodingComponent } from './encoding/encoding.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, EncodingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Angular';
}
