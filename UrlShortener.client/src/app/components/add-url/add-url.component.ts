import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { UrlShortenerService } from '../../shared/services/url-shortener.service';


@Component({
  selector: 'app-add-url',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './add-url.component.html',
  styleUrl: './add-url.component.css'
})
export class AddUrlComponent {
  reduceForm = this.formBuilder.group({
    url: ['']
  })

  constructor(
    private formBuilder: FormBuilder,
    private urlService: UrlShortenerService) {

  }

  onReduce() {
    let url = this.reduceForm.value.url as string;
    this.urlService.onReduce(url);
  }
}
