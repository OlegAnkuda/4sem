import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.css'
})
export class RegisterPageComponent implements OnInit, OnDestroy {

  "form": FormGroup
  "aSub": Subscription

  constructor(private auth: AuthService,
              private router: Router
  ) {}

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)])
    })
  }

  ngOnDestroy() {
    if(this.aSub) {
      this.aSub.unsubscribe()
    }
  }

  onSubmit() {
    this.form.disable()
    this.aSub = this.auth.register(this.form.value).subscribe(
      () => {
        this.router.navigate(['login'], {
          queryParams: {
            registred: true
          }
        })
      },
      error => {
        console.warn(error)
        this.form.enable()
      }
    )
  }

}
