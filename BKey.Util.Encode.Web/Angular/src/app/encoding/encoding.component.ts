import { Component, OnInit} from '@angular/core';
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

  constructor(private encodingService: EncodingService) {
  }

  ngOnInit() {
    this.encodingService.getSupportedEncodings()
      .subscribe(
        (encodings) => {
          this.encodings = encodings;
          if (this.encodings.length === 0) {
            this.selectedEncoding = 'No Encodings Available';
          }
        },
        (error) => {
          console.error('Failed to fetch supported encodings:', error);
          this.encodings = [];
          this.selectedEncoding = 'No Encodings Available';
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

}
