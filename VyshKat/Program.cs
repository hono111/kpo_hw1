using Microsoft.Extensions.DependencyInjection;
using VyshKat.Domain.Models.Equipment;
using VyshKat.Domain.Models.Vehicles;
using VyshKat.Services;
using VyshKat.Services.Interfaces;

var services = new ServiceCollection();

services.AddTransient<IInspectionService, ServiceCenter>();
services.AddSingleton<IParkManager, ParkManager>();

var serviceProvider = services.BuildServiceProvider();

var parkManager = serviceProvider.GetRequiredService<IParkManager>();

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("==================================================");
Console.WriteLine("   СЕРВИС КИКШЕРИНГА «ВЫШКАТ» — ПИЛОТНЫЙ ПАРК");
Console.WriteLine("==================================================\n");

Console.WriteLine("--- [1. ПРИЕМКА ТРАНСПОРТА И ТЕХОСМОТР] ---");

var scooterNoob = new ElectricScooter("SCOOT-001", "Ninebot Max G30", simplicityScore: 8, dailyEnergyConsumption: 1.5);
var scooterPro = new ElectricScooter("SCOOT-002", "Dualtron Ultra", simplicityScore: 3, dailyEnergyConsumption: 2.8);
var brokenScooter = new ElectricScooter("SCOOT-003", "Kugoo S3 (Неисправен)", simplicityScore: 7, dailyEnergyConsumption: 1.0);
var eBike = new ElectricBike("EBIKE-001", "Xiaomi Himo C26", simplicityScore: 6, dailyEnergyConsumption: 2.0);
var classicBike = new Bicycle("BIKE-001", "Format 5342 City", simplicityScore: 9);

RegisterVehicleWithLog(parkManager, scooterNoob);
RegisterVehicleWithLog(parkManager, scooterPro);
RegisterVehicleWithLog(parkManager, brokenScooter);
RegisterVehicleWithLog(parkManager, eBike);
RegisterVehicleWithLog(parkManager, classicBike);

Console.WriteLine();

Console.WriteLine("--- [2. ПОСТАНОВКА НА БАЛАНС ВЕЩЕЙ И ОБОРУДОВАНИЯ] ---");

var helmetL = new Helmet("HELM-001", "Шлем защитный городской (L)");
var station = new ChargingStation("STAT-001", "Быстрая зарядная станция Hub-4", dailyEnergyConsumption: 15.0);

parkManager.RegisterItem(helmetL);
parkManager.RegisterItem(station);
Console.WriteLine($"На баланс поставлена вещь: {helmetL.Title} (№ {helmetL.InventoryNumber})");
Console.WriteLine($"На баланс поставлена станция: {station.Title} (№ {station.InventoryNumber}, расход: {station.DailyEnergyConsumption} кВт·ч/сут)");

Console.WriteLine();

Console.WriteLine("--- [3. СУТОЧНЫЙ РАСХОД ЭНЕРГИИ] ---");
double totalEnergy = parkManager.GetTotalDailyEnergyConsumption();
Console.WriteLine($"Всего активного транспорта в парке: {parkManager.GetVehicles().Count} шт.");
Console.WriteLine($"Суммарное суточное потребление энергии флота и оборудования: {totalEnergy:F2} кВт·ч/сут");
Console.WriteLine("(Механические велосипеды и защитные шлемы не потребляют ресурс)");

Console.WriteLine();

Console.WriteLine("--- [4. ТРАНСПОРТ, ПОДХОДЯЩИЙ ДЛЯ НОВИЧКОВ (Простота >= 6)] ---");
var beginnerVehicles = parkManager.GetSuitableForBeginners();

foreach (var vehicle in beginnerVehicles)
{
    Console.WriteLine($" • [{vehicle.InventoryNumber}] {vehicle.Title} — Простота: {vehicle.SimplicityScore}/10");
}

Console.WriteLine();

Console.WriteLine("--- [5. ЕДИНЫЙ ИНВЕНТАРНЫЙ РЕЕСТР (Вся техника и вещи)] ---");
var allInventory = parkManager.GetAllInventory();

foreach (var item in allInventory)
{
    Console.WriteLine($" • Инв. №: {item.InventoryNumber,-12} | Наименование: {item.Title}");
}

static void RegisterVehicleWithLog(IParkManager manager, VyshKat.Domain.Models.Vehicle vehicle)
{
    bool accepted = manager.RegisterVehicle(vehicle);
    if (accepted)
    {
        Console.WriteLine($"[ОДОБРЕН] {vehicle.Title} (№ {vehicle.InventoryNumber}) прошел техосмотр и принят в парк.");
    }
    else
    {
        Console.WriteLine($"[ОТКАЗ] {vehicle.Title} (№ {vehicle.InventoryNumber}) ОТКЛОНЕН техосмотром!");
    }
}