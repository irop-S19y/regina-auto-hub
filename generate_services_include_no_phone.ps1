$json = Get-Content -Raw overpass_regina.json | ConvertFrom-Json
$elements = $json.elements
$out = New-Object System.Collections.Generic.List[string]
$count = 0
foreach ($el in $elements) {
    $tags = $el.tags
    if (-not $tags) { continue }
    $name = $tags.name
    if (-not $name) { continue }
    $phone = $tags.phone
    if (-not $phone) { $phone = $tags['contact:phone'] }
    $website = $tags.website
    $street = $tags['addr:street']
    $housenumber = $tags['addr:housenumber']
    $city = $tags['addr:city']
    if (-not $city) { $city = 'Regina' }
    $address = ''
    if ($housenumber) { $address += "$housenumber " }
    if ($street) { $address += $street }
    if (-not $address) { $address = $tags['addr:full'] }
    if (-not $address) { $address = 'Regina, SK' }

    # normalize phone to digits and add +1 for Saskatchewan area codes if needed
    $p = ''
    if ($phone) {
        $p = ($phone -replace '[^0-9+]','')
        if ($p -notmatch '^\\+1') {
            if ($p -match '^(306|639)') { $p = '+1' + $p }
        }
    }

    # escape double-quotes for C# string literals
    $safeName = $name.Replace('"','\\"')
    $safeAddress = $address.Replace('"','\\"')
    $safeCity = $city.Replace('"','\\"')
    $safeWebsite = ''
    if ($website) { $safeWebsite = $website.Replace('"','\\"') }

    $desc = 'Auto repair and maintenance services.'
    $img = '/images/services/placeholder.jpg'

    $entry = @"
,                new Service
                {
                    Name = "$safeName",
                    Address = "$safeAddress",
                    City = "$safeCity",
                    Phone = "$p",
                    Website = "$safeWebsite",
                    Description = "$desc",
                    WorkingHours = "Mon-Fri 8:00-17:00",
                    ImageUrl = "$img",
                    Rating = 4
                }
"@

    $out.Add($entry)
    $count++
    if ($count -ge 100) { break }
}
$out -join "`n" | Set-Content new_services_all_snippet.txt -Encoding utf8
Write-Output "Generated $count service entries to new_services_all_snippet.txt"