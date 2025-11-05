using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using System.Threading;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public async UniTask DestroyItself(bool correctHit)
    {
        Debug.Log("Je suis en train d'être détruit !!");
        GetComponent<BoxCollider2D>().isTrigger = false;

        var ct = new CancellationTokenSource();
        if (correctHit) await AnimateCorrectHit(ct);
        else await AnimateBadHit(ct);

        Destroy(gameObject);
    }

    private async UniTask AnimateCorrectHit(CancellationTokenSource ct)
    {
        await LSequence.Create()
            .Join(LMotion.Punch.Create(transform.position.x, .7f, .35f)
                .WithFrequency(10)
                .BindToPositionX(transform)
            )
            .Join(LMotion.Create(transform.position.y, transform.position.y + 1.5f, .35f)
                .BindToPositionY(transform)
            )
            .Join(LMotion.Create(1f, 0f, .35f)
                .BindToColorA(GetComponent<SpriteRenderer>())
            )
            .Run()
            .AddTo(gameObject)
            .ToUniTask(ct.Token);
    }

    private async UniTask AnimateBadHit(CancellationTokenSource ct)
    {
        await LSequence.Create()
            .Join(LMotion.Punch.Create(transform.position.x, .5f, .35f)
                .WithFrequency(12)
                .BindToPositionX(transform)
            )
            .Join(LMotion.Create(transform.position.y, transform.position.y - 1.5f, .35f)
                .BindToPositionY(transform)
            )
            .Join(LMotion.Create(1f, 0f, .35f)
                .BindToColorA(GetComponent<SpriteRenderer>())
            )
            .Run()
            .AddTo(gameObject)
            .ToUniTask(ct.Token);
    }
}