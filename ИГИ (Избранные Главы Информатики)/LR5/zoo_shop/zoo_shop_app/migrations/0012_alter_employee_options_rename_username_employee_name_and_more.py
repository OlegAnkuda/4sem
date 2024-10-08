# Generated by Django 5.0.4 on 2024-05-28 01:02

import django.db.models.deletion
import django.utils.timezone
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('zoo_shop_app', '0011_employeereg'),
    ]

    operations = [
        migrations.AlterModelOptions(
            name='employee',
            options={'ordering': ['name']},
        ),
        migrations.RenameField(
            model_name='employee',
            old_name='username',
            new_name='name',
        ),
        migrations.CreateModel(
            name='EmployeeUser',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('created', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('is_admin', models.BooleanField(default=0)),
                ('password', models.CharField(max_length=50)),
                ('employee_id', models.OneToOneField(on_delete=django.db.models.deletion.CASCADE, to='zoo_shop_app.employee')),
            ],
            options={
                'db_table': 'employees',
            },
        ),
        migrations.DeleteModel(
            name='EmployeeReg',
        ),
    ]
