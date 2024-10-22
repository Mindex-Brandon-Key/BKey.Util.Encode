import { Component, OnInit, Renderer2, RendererFactory2 } from '@angular/core';
import { EncodingService } from './encoding.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'app-encoding',
  templateUrl: './encoding.component.html',
  styleUrls: ['./encoding.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule
  ]
})
export class EncodingComponent implements OnInit {
  encodings: string[] = [];
  selectedEncoding: string = '';
  inputText: string = '';
  resultText: string = '';
  showCopyAlert: boolean = false;
  themeClass = 'light-mode';
  private renderer: Renderer2;

  constructor(private encodingService: EncodingService, private rendererFactory: RendererFactory2) {
    this.renderer = rendererFactory.createRenderer(null, null);
    this.detectTheme();
  }

  ngOnInit(): void {
    this.encodingService.getSupportedEncodings().subscribe(
      (data) => {
        this.encodings = data;
      },
      (error) => {
        console.error('Error fetching encodings:', error);
      }
    );
  }

  onEncode(): void {
    if (this.selectedEncoding && this.inputText) {
      this.encodingService.encodeText(this.selectedEncoding, this.inputText).subscribe(
        (result) => {
          this.resultText = result;
        },
        (error) => {
          this.resultText = 'Error: ' + error.error;
        }
      );
    }
  }

  copyToClipboard(): void {
    navigator.clipboard.writeText(this.resultText).then(
      () => {
        this.showCopyAlert = true;
        setTimeout(() => {
          this.showCopyAlert = false;
        }, 2000);
      },
      (err) => {
        console.error('Could not copy text: ', err);
      }
    );
  }

  detectTheme(): void {
    const prefersDarkScheme = window.matchMedia("(prefers-color-scheme: dark)");
    if (prefersDarkScheme.matches) {
      this.themeClass = 'dark-mode';
    } else {
      this.themeClass = 'light-mode';
    }

    prefersDarkScheme.addEventListener('change', (e) => {
      this.themeClass = e.matches ? 'dark-mode' : 'light-mode';
    });
  }

}
