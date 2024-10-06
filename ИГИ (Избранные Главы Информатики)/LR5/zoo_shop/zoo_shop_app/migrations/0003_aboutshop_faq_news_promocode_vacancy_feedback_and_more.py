# Generated by Django 5.0.4 on 2024-05-26 20:26

import django.core.validators
import django.db.models.deletion
import django.utils.timezone
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('zoo_shop_app', '0002_alter_product_table'),
    ]

    operations = [
        migrations.CreateModel(
            name='AboutShop',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('text', models.TextField(verbose_name='Shop description')),
                ('image', models.ImageField(upload_to='zoo_shop_app/static/main')),
            ],
            options={
                'db_table': 'about_shop',
            },
        ),
        migrations.CreateModel(
            name='FAQ',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('title', models.CharField(help_text='Question title', max_length=100)),
                ('text', models.TextField(help_text='Question text')),
            ],
            options={
                'db_table': 'questions',
                'ordering': ['create'],
            },
        ),
        migrations.CreateModel(
            name='News',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('title', models.CharField(help_text='Article title', max_length=100)),
                ('text', models.TextField(help_text='Article', max_length=1000)),
                ('image', models.ImageField(help_text='Article image', upload_to='app/static/news')),
            ],
            options={
                'db_table': 'news',
                'ordering': ['create'],
            },
        ),
        migrations.CreateModel(
            name='Promocode',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('discount', models.IntegerField(default=0, help_text='Discount', validators=[django.core.validators.MinValueValidator(1), django.core.validators.MaxValueValidator(100)])),
                ('archived', models.BooleanField(help_text='Archived or not')),
                ('promocode', models.CharField(default='zoobazar', help_text='Promocode', max_length=10)),
                ('text', models.CharField(help_text='Short description of stock', max_length=100)),
                ('valid_date', models.DateField(help_text='Valid until')),
            ],
            options={
                'db_table': 'stocks',
            },
        ),
        migrations.CreateModel(
            name='Vacancy',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('position', models.CharField(help_text='Position', max_length=100)),
                ('description', models.TextField(help_text='Job description')),
                ('salary', models.IntegerField(help_text='Monthly salary')),
            ],
            options={
                'db_table': 'vacancies',
                'ordering': ['salary'],
            },
        ),
        migrations.CreateModel(
            name='Feedback',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('text', models.TextField(help_text='Feedback text')),
                ('rate', models.IntegerField(default=10, help_text='Rate from 1 to 10', validators=[django.core.validators.MinValueValidator(1), django.core.validators.MaxValueValidator(10)])),
                ('author', models.ForeignKey(help_text='Client who left feedback', on_delete=django.db.models.deletion.CASCADE, to='zoo_shop_app.client')),
            ],
            options={
                'db_table': 'rates',
                'ordering': ['id'],
            },
        ),
        migrations.CreateModel(
            name='RegularClient',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('customer', models.ForeignKey(help_text='Client related', on_delete=django.db.models.deletion.CASCADE, to='zoo_shop_app.client')),
            ],
            options={
                'db_table': 'regular_clients',
            },
        ),
        migrations.CreateModel(
            name='ShoppingCart',
            fields=[
                ('id', models.BigAutoField(help_text='Unique id', primary_key=True, serialize=False)),
                ('create', models.DateTimeField(default=django.utils.timezone.now, help_text='Creation date')),
                ('update', models.DateTimeField(default=django.utils.timezone.now, help_text='Update date')),
                ('client', models.ForeignKey(help_text='Shopper', on_delete=django.db.models.deletion.CASCADE, to='zoo_shop_app.client')),
                ('product', models.ForeignKey(help_text='Cart product', on_delete=django.db.models.deletion.CASCADE, to='zoo_shop_app.product')),
            ],
            options={
                'db_table': 'shopping_carts',
                'ordering': ['id'],
            },
        ),
    ]
